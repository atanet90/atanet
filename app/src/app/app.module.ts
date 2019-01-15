import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler, APP_INITIALIZER } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { GlobalErrorHandler } from './global-error-handler';
import {
  MatButtonModule,
  MatToolbarModule,
  MatTabsModule,
  MatCardModule,
  MatInputModule,
  MatDialogModule,
  MatSnackBarModule,
  MatProgressSpinnerModule,
  MatChipsModule,
  MatProgressBarModule,
  MatSelectModule,
  MatIconModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatFormFieldModule,
  MatButtonToggleModule,
  MatExpansionModule,
  MatGridListModule,
  MatListModule,
  MatMenuModule,
  MatBadgeModule
} from '@angular/material';

import * as comp from './components';
import * as serv from './services';
import * as pipe from './pipes';
import { CommonModule } from '@angular/common';
import { ConfigService } from './config';
import { routing } from './app-routing.module';
import { AppComponent } from './app.component';
import { TokenInterceptor } from './token.interceptor';
import { AuthService } from './services';


export function init(_boot: ConfigService, authService: AuthService) {
  return () => {
    return _boot.loadConfig(authService);
  };
}

@NgModule({
  declarations: [
    AppComponent,
    comp.CreatePostComponent,
    comp.AllPostsComponent,
    comp.PostContainerComponent,
    comp.PostComponent,
    pipe.PrefixNumberPipe,
    pipe.SecurePipe,
    pipe.AtanetActionPipe,
    comp.CommentsComponent,
    comp.FileDialogComponent,
    comp.LoginComponent,
    comp.AtanetComponent,
    comp.UserItemComponent,
    comp.UserDetailComponent,
    comp.ScoreboardComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatToolbarModule,
    MatTabsModule,
    MatCardModule,
    MatInputModule,
    FormsModule,
    MatDialogModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatChipsModule,
    MatProgressBarModule,
    MatSelectModule,
    MatIconModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatButtonToggleModule,
    MatExpansionModule,
    MatGridListModule,
    MatListModule,
    MatMenuModule,
    MatMenuModule,
    MatBadgeModule,
    routing
  ],
  providers: [
    ConfigService,
    AuthService,
    {
      provide: APP_INITIALIZER,
      useFactory: init,
      deps: [ConfigService, AuthService],
      multi: true
    },
    serv.CreatePostService,
    serv.FilterPostService,
    serv.PostReactionService,
    serv.AtanetHttpService,
    serv.EventsService,
    serv.CommentHttpService,
    {
      provide: ErrorHandler,
      useClass: GlobalErrorHandler
    },
    serv.SnackbarService,
    serv.UserHttpService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    serv.AuthService
  ],
  entryComponents: [
    comp.CreatePostComponent,
    comp.FileDialogComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
