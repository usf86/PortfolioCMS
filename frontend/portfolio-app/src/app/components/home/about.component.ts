// src/app/components/about/about.component.ts
import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContentSection } from '../../models/portfolio-config.model';

/**
 * Affiche la section "A propos" d'un profil metier.
 * Recoit la ContentSection deja filtree par le parent (type === 'About').
 */
@Component({
  selector: 'app-about',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss']
})
export class AboutComponent {
  @Input({ required: true }) section!: ContentSection;
}