import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { TableComponent } from './pages/table/table.component';
import { TablesgetbyComponent } from './pages/tablesgetby/tablesgetby.component';

export const rootRouterConfig: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full'},
    { path: 'home', component: HomeComponent},
    { path: 'table', component: TableComponent },
    {path: 'tablesbyid', component: TablesgetbyComponent}
];