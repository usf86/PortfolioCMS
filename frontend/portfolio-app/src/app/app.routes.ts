// src/app/app.routes.ts
import { Routes } from '@angular/router';
import { LayoutComponent } from './components/layout/layout.component';
import { HomeComponent } from './components/home/home.component';
import { AboutComponent } from './components/about/about.component';
import { GalleryComponent } from './components/gallery/gallery.component';
import { ProductsComponent } from './components/products/products.component';
import { ContactComponent } from './components/contact/contact.component';

export const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', redirectTo: 'accueil', pathMatch: 'full' },
      { path: 'accueil', component: HomeComponent },
      { path: 'a-propos', component: AboutComponent },
      { path: 'galerie', component: GalleryComponent },
      { path: 'produits', component: ProductsComponent },
      { path: 'contact', component: ContactComponent }
    ]
  }
];