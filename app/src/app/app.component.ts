import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CreatePostComponent } from './components';
import { MatDialog } from '@angular/material';
import { ConfigService } from './config';
import { UserHttpService, EventsService, AuthService } from './services';
import { ShowUserInfo } from './model/show-user-info.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  public userInfo: ShowUserInfo;
  public googleUser: gapi.auth2.GoogleUser | undefined = undefined;
  public score: number | undefined = undefined;
  public scoreFailed = false;

  constructor(private dialog: MatDialog,
              private router: Router,
              private config: ConfigService,
              private userHttpService: UserHttpService,
              private eventsService: EventsService,
              private authService: AuthService) {
  }

  public get pictureUrl(): string {
    return this.config.config.baseUrl + 'users/picture';
  }

  public goToUserProfile(): void {
    this.router.navigate(['user/' + this.userInfo.id]);
  }

  public createPost(): void {
    const dialogRef = this.dialog.open(CreatePostComponent, {
      width: '70%'
    });
    dialogRef.afterClosed().subscribe(async result => {
      if (result > 0) {
        await this.refresh();
      }
    });
  }

  public async refresh(): Promise<void> {
    this.eventsService.triggerRefresh();
    this.setScore();
  }

  public ngOnInit(): void {
    this.authService.userInfo().then(user => {
      this.googleUser = user;
    });
    this.setScore();
    setInterval(() => {
      this.setScore();
    }, 10000);
  }

  private setScore(): void {
    this.userHttpService.getCurrentUserInfo(false).then(userInfo => {
      this.userInfo = userInfo;
      this.score = userInfo.score;
    }).catch(() => {
      this.scoreFailed = true;
    });
  }
}
