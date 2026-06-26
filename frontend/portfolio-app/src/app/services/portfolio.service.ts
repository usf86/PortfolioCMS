import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, shareReplay } from 'rxjs';
import { PortfolioConfig } from '../models/portfolio-config.model';
import { environment } from '../../environments/environment';

/**
 * Service responsable de recuperer la configuration complete du site
 * (un seul appel HTTP au demarrage de l'application).
 */
@Injectable({ providedIn: 'root' })
export class PortfolioService {
  private readonly apiUrl = `${environment.apiBaseUrl}/portfolio`;
  private cachedConfig$?: Observable<PortfolioConfig>;

  constructor(private http: HttpClient) {}

  /** Recupere la config via l'identifiant du profil metier. */
  getConfigById(id: number): Observable<PortfolioConfig> {
    return this.http.get<PortfolioConfig>(`${this.apiUrl}/${id}`);
  }

  /** Recupere la config via le type d'activite (utile en mode "site unique"). */
  getConfigByActivity(activityType: string): Observable<PortfolioConfig> {
    if (!this.cachedConfig$) {
      this.cachedConfig$ = this.http
        .get<PortfolioConfig>(`${this.apiUrl}/by-activity/${activityType}`)
        .pipe(shareReplay(1));
    }
    return this.cachedConfig$;
  }
}
