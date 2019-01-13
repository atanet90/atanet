import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AllPostsComponent } from '../all-posts';
import { AuthService } from '../../services';

@Component({
  selector: 'app-atanet',
  templateUrl: './atanet.component.html',
  styleUrls: ['./atanet.component.scss']
})
export class AtanetComponent implements OnInit {
  @ViewChild('all') public all: AllPostsComponent;
  public userEmail: string;

  constructor(private router: Router, private authService: AuthService) {
  }

  public ngOnInit(): void {
    this.all.isActive = true;
    this.authService.userInfo().then(authState => {
      if (!authState || !authState.getBasicProfile) {
        this.router.navigate(['/login']);
        return;
      }

      this.userEmail = authState.getBasicProfile().getEmail();
    }).catch(() => this.router.navigate(['/login']));
  }

}
