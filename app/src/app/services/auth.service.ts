import { Injectable } from '@angular/core';

@Injectable()
export class AuthService {
  private auth2: any;

  constructor() {
  }

  public setAuth2(auth2: any): void {
    this.auth2 = auth2;
  }

  public userInfo(): Promise<gapi.auth2.GoogleUser> {
    return new Promise((resolve, reject) => {
      this.auth2.then(() => {
        const userResult = this.auth2.isSignedIn.get();
        if (userResult) {
          const user = this.auth2.currentUser.get();
          resolve(user);
        } else {
          reject();
        }
      }).catch((error: any) => {
        reject(error);
      });
    });
  }

  public signIn(): Promise<void> {
    if (this.auth2) {
      return this.auth2.signIn();
    }
  }
}
