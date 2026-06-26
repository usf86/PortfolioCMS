import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductOrService } from '../../models/portfolio-config.model';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './products.component.html'
})
export class ProductsComponent {
  @Input() products: ProductOrService[] = [];

  get groupedProducts(): Record<string, ProductOrService[]> {
    return this.groupProductsByCategory(this.products ?? []);
  }

  private groupProductsByCategory(products: ProductOrService[]): Record<string, ProductOrService[]> {
    return products.reduce((groups, product) => {
      const category = product.category ?? 'Autres';
      (groups[category] ??= []).push(product);
      return groups;
    }, {} as Record<string, ProductOrService[]>);
  }
}
