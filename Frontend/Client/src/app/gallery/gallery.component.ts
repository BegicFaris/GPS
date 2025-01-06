import { Component, ElementRef, HostListener, inject, OnInit, ViewChild } from '@angular/core';
import { GalleryService } from '../_services/gallery.service';
import { Gallery } from '../_models/gallery';
import { AccountService } from '../_services/account.service';
import { LazyLoadDirective } from '../lazy-load.directive';
import { Title } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-gallery',
    standalone: true,
    imports: [ LazyLoadDirective, CommonModule, ],
    templateUrl: './gallery.component.html',
    styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {  
    @ViewChild('fileInput') fileInput!: ElementRef;

    private titleService = inject(Title);

    public accountService = inject(AccountService);



    selectedPhoto: Gallery | null = null;
    isModalOpen: boolean = false;
    isModalAnimating: boolean = false;
    photos: Gallery[] = []; // Holds the list of photos to display
    photoSend: any = {
        photoData: null, // Base64 string for image
        uploadDate: '',
    }

    constructor(private galleryService: GalleryService) {}

    ngOnInit(): void {
        this.titleService.setTitle("Gallery");
        this.getPhotos(); // Fetch existing photos on component load
    }

    @HostListener('window:keydown.escape')
    handleEscKey() {
        if (this.isModalOpen) {
            this.closeModal();
        }
    }

    openModal(photo: Gallery): void {
        this.selectedPhoto = photo;
        this.isModalOpen = true;
        this.isModalAnimating = true;
        document.body.style.overflow = 'hidden';
        // Reset animation state
        setTimeout(() => {
            this.isModalAnimating = false;
        }, 300);
    }

    closeModal(): void {
        this.isModalAnimating = true;
        setTimeout(() => {
            this.selectedPhoto = null;
            this.isModalOpen = false;
            this.isModalAnimating = false;
            document.body.style.overflow = 'auto';
        }, 200);
    }

    onModalClick(event: MouseEvent): void {
        if ((event.target as HTMLElement).classList.contains('modal-overlay')) {
            this.closeModal();
        }
    }

    // Fetch all photos from the backend
    getPhotos(): void {
        this.galleryService.getAllPhotos().subscribe((data) => {
            this.photos = data.map(photo => ({
                ...photo,
                photoData: 'data:image/jpeg;base64,' + photo.photoData // Append the base64 string to display the image
            }));
        });
    }

    // Handle file input for photo selection
    onFileSelected(event: Event): void {
        const input = event.target as HTMLInputElement;
        if (input.files && input.files[0]) {
            const file = input.files[0];
            const validImageTypes = ['image/jpeg', 'image/png', 'image/gif']; // Add allowed types

            if (!validImageTypes.includes(file.type)) {
                alert('Please upload a valid image file (JPEG, PNG, or GIF).');
                this.clearFileInput();
                return;
            }
            const reader = new FileReader();
            reader.onload = () => {
                // Convert file to base64 string
                this.photoSend.photoData = reader.result?.toString().split(',')[1]; // Base64 part
                const currentDate = new Date();
                const formattedDate = `${currentDate.getFullYear()}-${(currentDate.getMonth() + 1)
                    .toString()
                    .padStart(2, '0')}-${currentDate.getDate().toString().padStart(2, '0')}`;
                this.photoSend.uploadDate = formattedDate;
            };
            reader.readAsDataURL(file); // Read file as base64
        }
    }

    // Clear the file input after photo upload
    clearFileInput(): void {
        this.fileInput.nativeElement.value = ''; // Clear the file input value
        this.photoSend.photoData = null; // Reset the model's image property
    }

    // Handle upload button click
    onUpload(): void {
        if (this.photoSend.photoData) {
            const formData = this.photoSend;
            this.galleryService.uploadPhoto(formData).subscribe({
                next: (response) => {
                    this.getPhotos(); // Refresh the photo gallery
                    this.clearFileInput(); // Clear the file input after successful upload
                },
                error: (error) => {
                    console.log("Upload failed!", error);
                }
            });
        } else {
            alert('Please select a file before uploading.');
        }
    }

    trackByPhotoId(index: number, photo: Gallery): any {
        return photo.id;  // Track by unique ID
    }

    // Handle delete button click
    onDelete(id: number): void {
        this.galleryService.deletePhoto(id).subscribe({
            next: () => {
                // Remove the photo from the array after successful deletion
                this.photos = this.photos.filter(photo => photo.id !== id);
            },
            error: (error) => {
                console.error("Failed to delete photo", error);
            }
        });
    }
      
    
    // Handle drag and drop functionality
onDragStart(event: DragEvent, photo: Gallery): void {
    event.dataTransfer?.setData('photoId', photo.id.toString());
}

onDrop(event: DragEvent, targetPhoto: Gallery): void {
    event.preventDefault(); // Allow drop

    const draggedPhotoId = event.dataTransfer?.getData('photoId'); // Get dragged photo ID
    if (!draggedPhotoId) return;

    const draggedPhoto = this.photos.find(photo => photo.id.toString() === draggedPhotoId);
    if (!draggedPhoto) return;

    const draggedIndex = this.photos.indexOf(draggedPhoto);
    const targetIndex = this.photos.indexOf(targetPhoto);

    // Only swap if the dragged photo is different from the target photo
    if (draggedIndex !== targetIndex) {
        // Swap the photos
        this.photos[draggedIndex] = targetPhoto;
        this.photos[targetIndex] = draggedPhoto;

        // Now, create a new array with { id, newPosition }
        const updatedOrder = this.photos.map((photo, index) => ({
            id: photo.id,
            newPosition: index  // Assign new position based on the current index
        }));

        // Call the updatePhotoOrder API
        this.galleryService.updatePhotoOrder(updatedOrder).subscribe({
            next: () => {
                console.log('Photo order updated successfully.');
            },
            error: (err) => {
                console.error('Error updating photo order', err);
            }
        });
    }
}

onDragOver(event: DragEvent): void {
    event.preventDefault(); // Allow drop
}

}
