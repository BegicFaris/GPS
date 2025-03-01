import { Directive, ElementRef, Input, Renderer2, OnInit, OnDestroy } from '@angular/core';

@Directive({
  selector: '[appLazyLoad]',
  standalone: true,
})
export class LazyLoadDirective implements OnInit, OnDestroy {
  @Input() appLazyLoad!: string;

  private carouselListener?: () => void;
  private loadedImages: Set<HTMLImageElement> = new Set(); // Track loaded images

  constructor(private el: ElementRef, private renderer: Renderer2) {}

  ngOnInit() {
    const img = this.el.nativeElement as HTMLImageElement;

    if (!this.appLazyLoad) {
      console.error('No image source provided for lazy loading!');
      return;
    }

    // Handle carousel images
    const carouselItem = img.closest('.carousel-item') as HTMLElement;
    if (carouselItem) {
      const carousel = carouselItem.closest('.carousel') as HTMLElement;
      if (carousel) {
        this.handleCarouselImageLoading(carousel, carouselItem, img);
        return;
      }
    }

    // Fallback to IntersectionObserver for non-carousel images
    this.setupIntersectionObserver(img);
  }

  private handleCarouselImageLoading(carousel: HTMLElement, carouselItem: HTMLElement, img: HTMLImageElement) {
    // Load the image when the carousel item becomes active
    const loadImageIfActive = () => {
      if (carouselItem.classList.contains('active') && !this.loadedImages.has(img)) {
        this.loadImage(img);
      }
    };

    // Listen for the 'slid.bs.carousel' event to load the image when the carousel item becomes active
    carousel.addEventListener('slid.bs.carousel', () => {
      loadImageIfActive();
    });

    // Initial check for the active item when carousel is first loaded
    loadImageIfActive();
  }

  private setupIntersectionObserver(img: HTMLImageElement) {
    // IntersectionObserver for non-carousel images
    const observer = new IntersectionObserver(
      ([entry]) => {
        if (entry.isIntersecting) {
          this.loadImage(img);
          observer.unobserve(img);
        }
      },
      { rootMargin: '50px', threshold: 0.1 } // Load image when it's within 50px of the viewport
    );

    observer.observe(img);
  }

  private loadImage(img: HTMLImageElement) {
    if (img.src !== this.appLazyLoad) {
      this.renderer.setAttribute(img, 'src', this.appLazyLoad);
      this.loadedImages.add(img); // Mark as loaded
    }
  }

  private clearImage(img: HTMLImageElement) {
    // Clear the image source when the item is no longer active
    if (img.src === this.appLazyLoad) {
      this.renderer.setAttribute(img, 'src', ''); // Optionally, set to a placeholder or empty src
      this.loadedImages.delete(img); // Remove from loaded images set
    }
  }

  ngOnDestroy() {
    // Clean up the event listeners
    if (this.carouselListener) {
      this.carouselListener = undefined;
    }
  }
}
