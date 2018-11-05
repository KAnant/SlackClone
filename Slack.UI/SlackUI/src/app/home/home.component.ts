import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../shared/user.service';
import { User } from '../shared/user';
import {FilterPipe} from 'ngx-filter-pipe';
import { Router } from '@angular/router';
import * as $ from 'jquery';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

 users: User[];
  //account_Type: 'Admin';

  // update(value){
  //   this.account_Type = value;
  //   console.log (value);
  // }
  // selectChangeHandler (event: any){
  //   this.account_Type = event.target.value;
  // }
  userFilter: any = {name:''}
  constructor(private http: HttpClient, private userService: UserService, private filterPipe: FilterPipe, private router: Router) { 
    //console.log(filterPipe.transform(this.users, {name: 'a'}))
  }

  ngOnInit() {
    this.getUsers();
    //this.changeToAdmin(id);
    
  }
  getOwner(){
    this.userService.getOwner().subscribe(data => {
      this.users= data;
    });
  }

  getAdmin(){
    this.userService.getAdmin().subscribe(data => {
      this.users= data;
    });
  }

  getMember(){
    this.userService.getMember().subscribe(data => {
      this.users= data;
    });
  }
  getGuest(){
    this.userService.getGuest().subscribe(data => {
      this.users= data;
    });
  }

  getActive() {
    this.userService.getActive().subscribe(data => {
      this.users= data;
    });
  }

  getInactive() {
    this.userService.getInactive().subscribe(data => {
      this.users= data;
    });
  }
 
selectItem(){
  //alert('test')
    $('div.selectitem').find('input:checkbox').on('click', function () {
        $('.fullmembr > td').hide();
        $('div.selectitem').find('input:checked').each(function () {
            $('.fullmembr > td.' + $(this).attr('rel')).show();
        });
    });
};  
    
changeToAdmin(id) {
  alert("test")
  this.userService.getUsersById(id).subscribe(data =>{  
  this.userService.changeToAdmin(id).subscribe((data => {
    this.getUsers();
    console.log(this.users);
  }));
})
}
//  changeToAdmin() {
//    $("#adminbtn").click(function(){
//      $("#fullmem").text ("| Workspace Admin");
//      //$("table").load("table");

//      //location.reload();
//    });
//    //this.router.navigateByUrl[("home")];
//  }

 changeToGuest() {
  $("#guestbtn").click(function(){
    $("#fullmem").text ("| Guest");
    //$("table").load("table");

    //location.reload();
  });
  //this.router.navigateByUrl[("home")];
}

  getUsers() {
    this.userService.getUsers().subscribe((data => {
      this.users = data;
      console.log(this.users);
    }));
  }
}
