import { Component, ElementRef, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { PortfolioService } from '../../services/portfolio.service';
import { BusinessProfile } from '../../models/portfolio-config.model';

/**
 * Coquille persistante de l'application : navigation + zone de contenu routee.
 * Applique le theme (couleurs/police) une seule fois au chargement.
 */
@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {
  profile?: BusinessProfile;
  isMenuOpen = false;

  constructor(
    private portfolioService: PortfolioService,
    private hostElement: ElementRef
  ) {}

  ngOnInit(): void {
    const activityType = 'boulangerie'; // ou injecte dynamiquement (sous-domaine, config d'env...)

    this.portfolioService.getConfigByActivity(activityType).subscribe({
      next: (data) => {
        this.profile = data.profile;
        this.applyTheme(data.profile.primaryColor, data.profile.secondaryColor, data.profile.fontFamily);
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

  closeMenu(): void {
    this.isMenuOpen = false;
  }
}
