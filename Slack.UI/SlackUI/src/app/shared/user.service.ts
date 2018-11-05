import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import {User} from '../shared/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http:HttpClient) { }

  user: Observable<User[]>
  id: User[];

  getUsers() {
    return this.http.get<User[]>(`${environment.apiUrl}user/getUsers`);
  }

  
  getUsersById(id: any) {
    return this.http.get<User[]>(`${environment.apiUrl}user/getUsersById/${id}`);
   
  }

 sendEmail(emailList) {
    return this.http.post(`${environment.apiUrl}/user/sendInvitation/`, emailList);
  }

  getOwner(){
    return this.http.get<User[]>(`${environment.apiUrl}/user/getOwner/`);
  }

  getAdmin(){
    return this.http.get<User[]>(`${environment.apiUrl}/user/getAdmin/`);
  }

  getMember(){
    return this.http.get<User[]>(`${environment.apiUrl}/user/getMem/`);
  }

  getGuest(){
    return this.http.get<User[]>(`${environment.apiUrl}/user/getGuest/`);
  }

  getActive(){
    return this.http.get<User[]>(`${environment.apiUrl}/user/getActive/`);
  }

  getInactive(){
    return this.http.get<User[]>(`${environment.apiUrl}/user/getInactive/`);
  }

  changeToAdmin(id) {
    return this.http.get<User>(`${environment.apiUrl}/user/ChangeToAdmin/${id}`);
   
  }
}
