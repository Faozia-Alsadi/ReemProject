import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TranslatePipe, TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TranslatePipe],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit {
  constructor(private translate: TranslateService) {}

  ngOnInit(): void {
    const saved = localStorage.getItem('lang') ?? 'ar';
    this.setLanguage(saved);
  }

  setLanguage(lang: string): void {
    this.translate.use(lang);
    document.documentElement.lang = lang;
    document.documentElement.dir = lang === 'ar' ? 'rtl' : 'ltr';
    localStorage.setItem('lang', lang);
  }
}
