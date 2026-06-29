// src/app/components/gallery/gallery.component.ts
import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContentSection } from '../../models/portfolio-config.model';

/**
 * Affiche la galerie d'images d'un profil metier.
 * Recoit la ContentSection deja filtree par le parent (type === 'Gallery').
 */
@Component({
  selector: 'app-gallery',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.scss']
})
export class GalleryComponent {
  @Input({ required: true }) section!: ContentSection;
}