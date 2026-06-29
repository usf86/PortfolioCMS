// src/app/components/contact/contact.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { PortfolioService } from '../../services/portfolio.service';
import { BusinessProfile, OpeningHour } from '../../models/portfolio-config.model';

/** Page "Contact" : formulaire + carte Google Maps + adresse + horaires. */
@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {
  profile?: BusinessProfile;
  openingHours: OpeningHour[] = [];
  safeMapUrl?: SafeResourceUrl;

  // Modele du formulaire de contact
  contactForm = {
    name: '',
    email: '',
    message: ''
  };
  isSubmitted = false;

  constructor(
    private portfolioService: PortfolioService,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    this.portfolioService.getConfigByActivity('boulangerie').subscribe({
      next: (data) => {
        this.profile = data.profile;
        this.openingHours = data.openingHours;

        // L'iframe Google Maps necessite une URL "de confiance" pour Angular
        if (data.profile.googleMapsEmbedUrl) {
          this.safeMapUrl = this.sanitizer.bypassSecurityTrustResourceUrl(data.profile.googleMapsEmbedUrl);
        }
      }
    });
  }

  /**
   * Soumission du formulaire.
   * NOTE : aucun endpoint backend d'envoi d'email n'existe encore.
   * Pour l'instant, on simule la confirmation cote client.
   * A remplacer par un vrai appel API (POST /api/contact) relié a un service d'email (SMTP/SendGrid).
   */
  onSubmit(form: NgForm): void {
    if (form.invalid) return;

    console.log('Formulaire de contact soumis :', this.contactForm);
    this.isSubmitted = true;
    form.resetForm();
  }
}