import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../core/api';
import { AuthService } from '../../core/auth';
import { DashboardStats } from '../../core/models';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule],
  templateUrl: './dashboard.html',
  styles: ``
})
export class Dashboard implements OnInit {
  stats: DashboardStats | null = null;
  loading = true;

  constructor(public api: ApiService, public auth: AuthService) {}

  ngOnInit() {
    this.api.getStats().subscribe({
      next: s => { this.stats = s; this.loading = false; },
      error: () => { this.loading = false; }
    });
  }

  pct(val: number, total: number): number {
    return total > 0 ? Math.round((val / total) * 100) : 0;
  }
}
