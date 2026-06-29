import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortfolioService } from '../../services/portfolio.service';
import { ProductOrService } from '../../models/portfolio-config.model';

/** Page "Produits" - autonome, recupere sa propre donnee via le service. */
@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {
  products: ProductOrService[] = [];

  constructor(private portfolioService: PortfolioService) {}

  ngOnInit(): void {
    this.portfolioService.getConfigByActivity('boulangerie').subscribe({
      next: (data) => {
        this.products = data.products;
      }
    });
  }

  /** Regroupe les produits par categorie pour l'affichage en sections. */
  groupProductsByCategory(products: ProductOrService[]): Record<string, ProductOrService[]> {
    return products.reduce((groups, product) => {
      (groups[product.category] ??= []).push(product);
      return groups;
    }, {} as Record<string, ProductOrService[]>);
  }
}
