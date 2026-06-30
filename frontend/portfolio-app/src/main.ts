import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { register } from 'swiper/element/bundle';

register(); // enregistre <swiper-container> et <swiper-slide> comme elements HTML natifs

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
