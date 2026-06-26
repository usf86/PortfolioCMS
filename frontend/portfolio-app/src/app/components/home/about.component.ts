import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContentSection } from '../../models/portfolio-config.model';

@Component({
  selector: 'app-about',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './about.component.html'
})
export class AboutComponent {
  @Input() section?: ContentSection;
}
