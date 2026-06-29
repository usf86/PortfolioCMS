// src/app/components/contact/contact.component.ts
import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BusinessProfile, OpeningHour } from '../../models/portfolio-config.model';

/**
 * Affiche les informations de contact et les horaires d'ouverture.
 * Utilise comme footer de la page.
 */
@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent {
  @Input({ required: true }) profile!: BusinessProfile;
  @Input({ required: true }) openingHours: OpeningHour[] = [];
}