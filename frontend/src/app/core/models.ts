export interface LoginResult {
  token: string;
  fullNameAr: string;
  fullNameEn: string;
  role: string;
  userId: number;
}

export interface ValueDto {
  id: number;
  nameAr: string;
  nameEn: string;
  color: string;
  iconClass: string;
  displayOrder: number;
  projectCount: number;
}

export interface ProjectDto {
  id: number;
  nameAr: string;
  nameEn: string;
  descriptionAr?: string;
  status: number;
  statusAr: string;
  valueId: number;
  valueNameAr: string;
  valueColor: string;
  managerId?: number;
  managerNameAr?: string;
  department?: string;
  startDate?: string;
  expectedEndDate?: string;
  actualEndDate?: string;
  notes?: string;
}

export interface UserDto {
  id: number;
  fullNameAr: string;
  fullNameEn: string;
  email: string;
  department: string;
  role: number;
  projectCount: number;
}

export interface DashboardStats {
  totalProjects: number;
  notStarted: number;
  inProgress: number;
  completed: number;
  byValue: ValueStat[];
  recentProjects: RecentProject[];
}

export interface ValueStat {
  nameAr: string;
  color: string;
  total: number;
  completed: number;
  inProgress: number;
  notStarted: number;
}

export interface RecentProject {
  id: number;
  nameAr: string;
  statusAr: string;
  valueNameAr: string;
  valueColor: string;
  managerNameAr?: string;
}
