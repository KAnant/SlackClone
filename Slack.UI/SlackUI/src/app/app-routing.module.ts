import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { InviteComponent } from './invite/invite.component';


const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'invite', component: InviteComponent },
  { path: '', component: HomeComponent },

  
];
@NgModule({
  // imports: [
  //   CommonModule
  // ],
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ],
   declarations: []
})


export class AppRoutingModule { }
