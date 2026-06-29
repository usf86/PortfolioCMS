import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductOrService } from '../../models/portfolio-config.model';

/**
 * Affiche les produits/services groupes par categorie.
 * Recoit la liste brute de produits ; la logique de regroupement
 * est encapsulee ici (et non plus dans le composant parent).
 */
@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent {
  @Input({ required: true }) products: ProductOrService[] = [];

  /** Regroupe les produits par categorie pour l'affichage en sections. */
  groupProductsByCategory(products: ProductOrService[]): Record<string, ProductOrService[]> {
    return products.reduce((groups, product) => {
      (groups[product.category] ??= []).push(product);
      return groups;
    }, {} as Record<string, ProductOrService[]>);
  }
}