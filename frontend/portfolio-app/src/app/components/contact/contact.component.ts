import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortfolioService } from '../../services/portfolio.service';
import { BusinessProfile, OpeningHour } from '../../models/portfolio-config.model';

/** Page "Contact" - autonome, recupere sa propre donnee via le service. */
@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {
  profile?: BusinessProfile;
  openingHours: OpeningHour[] = [];

  constructor(private portfolioService: PortfolioService) {}

  ngOnInit(): void {
    this.portfolioService.getConfigByActivity('boulangerie').subscribe({
      next: (data) => {
        this.profile = data.profile;
        this.openingHours = data.openingHours;
      }
    });
  }
}
