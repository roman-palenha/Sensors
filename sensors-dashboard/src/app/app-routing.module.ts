import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ScannersComponent } from './components/scanners/scanners.component';
import { StatisticsComponent } from './components/statistics/statistics.component';

const routes: Routes = [
  { path: 'scanners', component: ScannersComponent },
  { path: 'statistics', component: StatisticsComponent },
  { path: '', redirectTo: '/scanners', pathMatch: 'full' },
  { path: '**', redirectTo: '/scanners' }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
