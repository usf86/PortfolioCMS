/** Represente la configuration metier (couleurs, contact, etc.) */
export interface BusinessProfile {
  id: number;
  businessName: string;
  activityType: string;
  slogan: string;
  logoUrl?: string;
  primaryColor: string;
  secondaryColor: string;
  fontFamily: string;
  address?: string;
  city?: string;
  phone?: string;
  email?: string;
  facebookUrl?: string;
  instagramUrl?: string;
  googleMapsEmbedUrl?: string;
}

export interface OpeningHour {
  day: string;
  openMorning?: string;
  closeMorning?: string;
  openAfternoon?: string;
  closeAfternoon?: string;
  isClosed: boolean;
}

export type SectionType = 'Hero' | 'About' | 'Gallery' | 'Services' | 'Contact' | 'CustomText';

export interface ContentSection {
  type: SectionType;
  title: string;
  subtitle?: string;
  body?: string;
  imageUrl?: string;
  galleryImages: string[];
  displayOrder: number;
}

export interface ProductOrService {
  title: string;
  description?: string;
  price?: number;
  priceUnit?: string;
  imageUrl?: string;
  category: string;
  isFeatured: boolean;
}

export interface Testimonial {
  authorName: string;
  comment: string;
  rating: number;
}

/** DTO racine renvoye par l'API */
export interface PortfolioConfig {
  profile: BusinessProfile;
  openingHours: OpeningHour[];
  sections: ContentSection[];
  products: ProductOrService[];
  testimonials: Testimonial[];
}
