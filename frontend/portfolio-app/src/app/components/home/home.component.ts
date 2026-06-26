import { Component, OnInit, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortfolioService } from '../../services/portfolio.service';
import { PortfolioConfig, ContentSection, ProductOrService } from '../../models/portfolio-config.model';

/**
 * Page d'accueil dynamique : se construit entierement a partir de la
 * configuration metier recue de l'API (couleurs, sections, produits...).
 */
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  config?: PortfolioConfig;
  isLoading = true;
  errorMessage = '';

  constructor(
    private portfolioService: PortfolioService,
    private hostElement: ElementRef
  ) {}

  ngOnInit(): void {
    // Exemple : le type d'activite pourrait venir d'une config d'environnement
    // (deploiement dedie par client) ou d'un sous-domaine.
    const activityType = 'boulangerie'; // ou injecte dynamiquement

    this.portfolioService.getConfigByActivity(activityType).subscribe({
      next: (data) => {
        this.config = data;
        this.applyTheme(data.profile.primaryColor, data.profile.secondaryColor, data.profile.fontFamily);
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'Impossible de charger les informations du site.';
        this.isLoading = false;
      }
    });
  }

  /** Injecte les couleurs du client comme variables CSS sur l'element racine. */
  private applyTheme(primary: string, secondary: string, font: string): void {
    const root = this.hostElement.nativeElement as HTMLElement;
    root.style.setProperty('--color-primary', primary);
    root.style.setProperty('--color-secondary', secondary);
    root.style.setProperty('--font-family-base', font);
  }

  /** Regroupe les produits par categorie pour l'affichage en sections. */
  groupProductsByCategory(products: ProductOrService[]): Record<string, ProductOrService[]> {
    return products.reduce((groups, product) => {
      (groups[product.category] ??= []).push(product);
      return groups;
    }, {} as Record<string, ProductOrService[]>);
  }

  trackBySection(_index: number, section: ContentSection): string {
    return section.title;
  }
}
