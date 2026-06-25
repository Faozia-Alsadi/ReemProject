import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../core/api';
import { UserDto } from '../../core/models';

declare const bootstrap: any;

@Component({
  selector: 'app-users',
  imports: [CommonModule, FormsModule],
  templateUrl: './users.html',
  styles: ``
})
export class Users implements OnInit {
  users: UserDto[] = [];
  loading = true;
  saving = false;
  editMode = false;
  form: Partial<UserDto> & { id?: number; password?: string } = {};

  constructor(public api: ApiService) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.loading = true;
    this.api.getUsers().subscribe({
      next: u => { this.users = u; this.loading = false; },
      error: () => { this.loading = false; }
    });
  }

  openAdd() {
    this.editMode = false;
    this.form = { role: 1 };
    this.showModal();
  }

  openEdit(u: UserDto) {
    this.editMode = true;
    this.form = { ...u, password: '' };
    this.showModal();
  }

  save() {
    this.saving = true;
    const payload = { ...this.form };
    if (!payload.password) delete payload.password;
    this.api.upsertUser(payload).subscribe({
      next: () => { this.saving = false; this.hideModal(); this.load(); },
      error: () => { this.saving = false; }
    });
  }

  roleLabel(role: number) {
    return role === 0 ? 'مدير النظام' : 'مدير مشروع';
  }

  private showModal() {
    const el = document.getElementById('userModal');
    if (el) bootstrap.Modal.getOrCreateInstance(el).show();
  }

  private hideModal() {
    const el = document.getElementById('userModal');
    if (el) bootstrap.Modal.getInstance(el)?.hide();
  }
}
