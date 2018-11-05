import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { UserService } from '../shared/user.service';
import { Email } from '../shared/user';

@Component({
  selector: 'app-invite',
  templateUrl: './invite.component.html',
  styleUrls: ['./invite.component.css']
})
export class InviteComponent implements OnInit {

  constructor(private userService: UserService) { }
  containers = [];
  public emails: Email = new Email();
  ngOnInit() {

    this.emails.fromAdress = 'kanantiw@gmail.com';
    this.emails.Subject = 'Slack Invitation';
    this.emails.Content = 'Please Join Slack!';
  }

  sendEmail(sendInvitaionForm) {
    const emailList = [];
    if (sendInvitaionForm.email_address0 !== undefined && sendInvitaionForm.email_address0 !== "") {
      emailList.push(sendInvitaionForm.email_address0);
    }
    if (sendInvitaionForm.email_address1 !== undefined && sendInvitaionForm.email_address1 !== "") {
      emailList.push(sendInvitaionForm.email_address1);
    }
    this.userService.sendEmail(emailList).subscribe(res => {
      alert('Email send successfully');
    });
  }
  add() {
    this.containers.push(this.containers.length);
  }

  closeBlock() {
    const elem = document.getElementById('show-block');
    elem.remove();
  }

 
}
