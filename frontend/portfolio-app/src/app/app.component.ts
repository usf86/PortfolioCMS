import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

/** Composant racine - se contente d'afficher la route active. */
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  template: `<router-outlet></router-outlet>`
})
export class AppComponent {}
