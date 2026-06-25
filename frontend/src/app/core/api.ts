import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { DashboardStats, ProjectDto, UserDto, ValueDto } from './models';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class ApiService {
  private base = environment.apiUrl;

  constructor(private http: HttpClient) {}

  // Dashboard
  getStats() {
    return this.http.get<DashboardStats>(`${this.base}/dashboard/stats`);
  }

  // Values
  getValues() {
    return this.http.get<ValueDto[]>(`${this.base}/values`);
  }

  // Projects
  getProjects(valueId?: number, status?: number) {
    let params = new HttpParams();
    if (valueId != null) params = params.set('valueId', valueId);
    if (status  != null) params = params.set('status', status);
    return this.http.get<ProjectDto[]>(`${this.base}/projects`, { params });
  }

  upsertProject(data: Partial<ProjectDto> & { id?: number }) {
    return this.http.post<{ id: number }>(`${this.base}/projects`, data);
  }

  deleteProject(id: number) {
    return this.http.delete(`${this.base}/projects/${id}`);
  }

  // Users
  getUsers() {
    return this.http.get<UserDto[]>(`${this.base}/users`);
  }

  upsertUser(data: Partial<UserDto> & { id?: number; password?: string }) {
    return this.http.post<{ id: number }>(`${this.base}/users`, data);
  }
}
