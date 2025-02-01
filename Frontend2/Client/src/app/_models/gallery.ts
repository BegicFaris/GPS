// gallery.model.ts
export interface Gallery {
    id: number;
    photoData: string;  // Base64 string for image
    uploadDate: Date;
    position: number;
}
