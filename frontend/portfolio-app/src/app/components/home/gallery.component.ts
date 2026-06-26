import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContentSection } from '../../models/portfolio-config.model';

@Component({
  selector: 'app-gallery',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './gallery.component.html'
})
export class GalleryComponent {
  @Input() section?: ContentSection;
}
