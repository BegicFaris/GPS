import { Component, ElementRef, HostListener, inject, OnInit, ViewChild } from '@angular/core';
import { GalleryService } from '../_services/gallery.service';
import { Gallery } from '../_models/gallery';
import { AccountService } from '../_services/account.service';


@Component({
    selector: 'app-gallery',
    templateUrl: './gallery.component.html',
    styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {  
    @ViewChild('fileInput') fileInput!: ElementRef;

    public accountService = inject(AccountService);

    selectedPhoto: Gallery | null = null;
    isModalOpen: boolean = false;
    isModalAnimating: boolean = false;
    photos: Gallery[]|null=[]; // Holds the list of photos to display
    photoSend:any={
      photoData: null, // Base64 string for image
      uploadDate: '',
    }

    constructor(private galleryService: GalleryService) {}

    ngOnInit(): void {
        this.getPhotos(); // Fetch existing photos on component load
        console.log(this.photos);
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
            // Assuming the photoData returned from the backend is a base64 string
            this.photos = data.map(photo => ({
                ...photo,
                photoData: 'data:image/jpeg;base64,' + photo.photoData // Append the base64 string to display the image
            }));
        });
    }

    onFileSelected(event: Event): void {
      const input = event.target as HTMLInputElement;
      if (input.files && input.files[0]) {
        const file = input.files[0];
        const validImageTypes = ['image/jpeg', 'image/png', 'image/gif']; // Add allowed types
  
        if (!validImageTypes.includes(file.type)) {
          alert('Please upload a valid image file (JPEG, PNG, or GIF).');
          this.clearFileInput(); // Automatically clear the file input
          return;
        }
        const reader = new FileReader();
        reader.onload = () => {
          // Convert file to base64 string and store in model.image
          this.photoSend.photoData = reader.result?.toString().split(',')[1]; // Base64 part
          const currentDate = new Date();
          const formattedDate = `${currentDate.getFullYear()}-${(currentDate.getMonth() + 1)
            .toString()
            .padStart(2, '0')}-${currentDate.getDate().toString().padStart(2, '0')}`;
          this.photoSend.uploadDate=formattedDate;
        };
        reader.readAsDataURL(file); // Read file as base64
      }
    }
  
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
  

    // Handle delete button click
    onDelete(id: number): void {
        this.galleryService.deletePhoto(id).subscribe(() => {
            // Remove the deleted photo from the
            this.getPhotos();
        });
    }
}
