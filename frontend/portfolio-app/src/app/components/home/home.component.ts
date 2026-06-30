// src/app/components/home/home.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { PortfolioService } from '../../services/portfolio.service';
import { BusinessProfile, Testimonial, ProductOrService } from '../../models/portfolio-config.model';

/** Page d'accueil : hero, produits vedettes, section atouts, temoignages, CTA. */
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  profile?: BusinessProfile;
  testimonials: Testimonial[] = [];
  featuredProducts: ProductOrService[] = [];
  isLoading = true;
  errorMessage = '';

  constructor(private portfolioService: PortfolioService) {}

  ngOnInit(): void {
    this.portfolioService.getConfigByActivity('boulangerie').subscribe({
      next: (data) => {
        this.profile = data.profile;
        this.testimonials = data.testimonials;
        this.featuredProducts = data.products.filter(p => p.isFeatured);
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'Impossible de charger les informations du site.';
        this.isLoading = false;
      }
    });
  }
}