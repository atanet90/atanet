import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ConfigService } from '../../config';
import { AuthService } from '../../services';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  constructor(private router: Router, private config: ConfigService, private authService: AuthService) { }

  public url = '';

  public ngOnInit(): void {
    this.url = this.config.config.baseUrl + 'files/expression';
    this.authService.userInfo().then(userInfo => {
      if (userInfo) {
        this.router.navigate(['/']);
      }
    });
  }

  public login(): void {
    this.authService.signIn();
  }

}
