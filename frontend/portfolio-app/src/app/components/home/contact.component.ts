import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BusinessProfile, OpeningHour } from '../../models/portfolio-config.model';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './contact.component.html'
})
export class ContactComponent {
  @Input() profile?: BusinessProfile;
  @Input() openingHours: OpeningHour[] = [];
}
