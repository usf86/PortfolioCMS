import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortfolioService } from '../../services/portfolio.service';
import { BusinessProfile, Testimonial } from '../../models/portfolio-config.model';

/** Page d'accueil : hero + temoignages. */
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  profile?: BusinessProfile;
  testimonials: Testimonial[] = [];
  isLoading = true;
  errorMessage = '';

  constructor(private portfolioService: PortfolioService) {}

  ngOnInit(): void {
    this.portfolioService.getConfigByActivity('boulangerie').subscribe({
      next: (data) => {
        this.profile = data.profile;
        this.testimonials = data.testimonials;
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'Impossible de charger les informations du site.';
        this.isLoading = false;
      }
    });
  }
}
