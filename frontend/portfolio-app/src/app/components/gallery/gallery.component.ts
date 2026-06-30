// src/app/components/gallery/gallery.component.ts
import { Component, OnInit, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortfolioService } from '../../services/portfolio.service';
import { ContentSection } from '../../models/portfolio-config.model';

/** Page "Galerie" - autonome, recupere sa propre donnee via le service. */
@Component({
  selector: 'app-gallery',
  standalone: true,
  imports: [CommonModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],   // ← necessaire pour <swiper-container>/<swiper-slide>
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.scss']
})
export class GalleryComponent implements OnInit {
  section?: ContentSection;

  constructor(private portfolioService: PortfolioService) {}

  ngOnInit(): void {
    this.portfolioService.getConfigByActivity('boulangerie').subscribe({
      next: (data) => {
        this.section = data.sections.find(s => s.type === 'Gallery');
      }
    });
  }
}