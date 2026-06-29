import { Component, OnInit, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortfolioService } from '../../services/portfolio.service';
import { PortfolioConfig, ContentSection } from '../../models/portfolio-config.model';
import { AboutComponent } from './about.component';
import { GalleryComponent } from './gallery.component';
import { ProductsComponent } from './products.component';
import { ContactComponent } from './contact.component';

/**
 * Page d'accueil : recupere la configuration et orchestre les sous-composants.
 * La logique d'affichage de chaque section est deleguee aux composants enfants.
 */
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, AboutComponent, GalleryComponent, ProductsComponent, ContactComponent],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  config?: PortfolioConfig;
  isLoading = true;
  errorMessage = '';
  isMenuOpen = false;

  constructor(
    private portfolioService: PortfolioService,
    private hostElement: ElementRef
  ) {}

  ngOnInit(): void {
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

  /** Recupere la section 'About' parmi les sections dynamiques. */
  get aboutSection() {
    return this.config?.sections.find(s => s.type === 'About');
  }

  /** Recupere la section 'Gallery' parmi les sections dynamiques. */
  get gallerySection() {
    return this.config?.sections.find(s => s.type === 'Gallery');
  }

  /** Fait défiler vers une section ancrée dans la page. */
  scrollToSection(sectionId: string, event: Event): void {
    event.preventDefault();
    const section = document.getElementById(sectionId);
    if (section) {
      section.scrollIntoView({ behavior: 'smooth', block: 'start' });
      this.isMenuOpen = false;
    }
  }
}
