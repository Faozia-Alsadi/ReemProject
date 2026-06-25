import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../core/api';
import { AuthService } from '../../core/auth';
import { ProjectDto, ValueDto, UserDto } from '../../core/models';

declare const bootstrap: any;

@Component({
  selector: 'app-projects',
  imports: [CommonModule, FormsModule],
  templateUrl: './projects.html',
  styles: ``
})
export class Projects implements OnInit {
  projects: ProjectDto[] = [];
  values: ValueDto[] = [];
  users: UserDto[] = [];

  filterValueId: number | null = null;
  filterStatus: number | null = null;

  loading = true;
  saving = false;
  deleting: number | null = null;

  editMode = false;
  form: Partial<ProjectDto> & { id?: number; password?: string } = {};

  statusOptions = [
    { value: 0, label: 'لم تبدأ' },
    { value: 1, label: 'قيد التنفيذ' },
    { value: 2, label: 'مكتمل' }
  ];

  constructor(public api: ApiService, public auth: AuthService) {}

  ngOnInit() {
    this.load();
    this.api.getValues().subscribe(v => this.values = v);
    if (this.auth.isAdmin()) {
      this.api.getUsers().subscribe(u => this.users = u);
    }
  }

  load() {
    this.loading = true;
    this.api.getProjects(
      this.filterValueId ?? undefined,
      this.filterStatus ?? undefined
    ).subscribe({
      next: p => { this.projects = p; this.loading = false; },
      error: () => { this.loading = false; }
    });
  }

  get grouped(): { value: ValueDto; projects: ProjectDto[] }[] {
    const visibleValues = this.filterValueId
      ? this.values.filter(v => v.id === this.filterValueId)
      : this.values;
    return visibleValues.map(v => ({
      value: v,
      projects: this.projects.filter(p => p.valueId === v.id)
    })).filter(g => g.projects.length > 0);
  }

  statusLabel(s: number) {
    return this.statusOptions.find(o => o.value === s)?.label ?? '';
  }

  statusColor(s: number) {
    return s === 2 ? '#27ae60' : s === 1 ? '#e67e22' : '#aaa';
  }

  openAdd() {
    this.editMode = false;
    this.form = { status: 0 };
    this.showModal();
  }

  openEdit(p: ProjectDto) {
    this.editMode = true;
    this.form = { ...p };
    this.showModal();
  }

  save() {
    this.saving = true;
    this.api.upsertProject(this.form).subscribe({
      next: () => { this.saving = false; this.hideModal(); this.load(); },
      error: () => { this.saving = false; }
    });
  }

  confirmDelete(id: number) {
    if (!confirm('هل أنت متأكد من حذف هذا المشروع؟')) return;
    this.deleting = id;
    this.api.deleteProject(id).subscribe({
      next: () => { this.deleting = null; this.load(); },
      error: () => { this.deleting = null; }
    });
  }

  private showModal() {
    const el = document.getElementById('projectModal');
    if (el) bootstrap.Modal.getOrCreateInstance(el).show();
  }

  private hideModal() {
    const el = document.getElementById('projectModal');
    if (el) bootstrap.Modal.getInstance(el)?.hide();
  }
}
