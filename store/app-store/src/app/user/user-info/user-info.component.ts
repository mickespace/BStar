import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { User } from '../../shared/model/user-model';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.css']
})
export class UserInfoComponent implements OnInit {
  public currentUser: User;
  constructor(public titleService: Title) { }

  ngOnInit() {
    this.setTitle('个人中心');
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    if (!this.currentUser.PhoneNumber) {
      this.currentUser.PhoneNumber = '无';
    } else {
      this.currentUser.PhoneNumber = this.formatPhone(this.currentUser.PhoneNumber);
    }
    if (!this.currentUser.Email) {
      this.currentUser.Email = '无';
    } else {
      this.currentUser.Email = this.formatEmail(this.currentUser.Email);
    }
  }
  public formatPhone(phone: string): string {
    const reg = /(\d{3})\d{4}(\d{4})/;
    return phone.replace(reg, '$1****$2');
  }
  public formatEmail(email: string): string {
    const reg = /(.{2}).+(.{2}@.+)/g;
    return email.replace(reg, '$1****$2');
  }
  /**
  * 设置title
  * @param newTitle title
  */
  public setTitle(newTitle: string) {
    this.titleService.setTitle(newTitle);
  }
}
