import { Injectable, Injector } from '@angular/core';
import { AtanetHttpService } from './atanet-http.service';
import { ShowUserInfo } from '../model/show-user-info.model';
import { Request } from '../model';
import { UserWithScore } from '../model/user-with-score.model';

@Injectable()
export class UserHttpService {

  constructor(private readonly httpService: AtanetHttpService) {
  }

  public async getCurrentUserInfo(handleError = true): Promise<ShowUserInfo> {
    const uri = 'users';
    return await this.httpService.get(uri, ShowUserInfo, handleError);
  }

  public async getUserInfo(userId: number, handleError = true): Promise<ShowUserInfo> {
    const uri = `users/${userId}`;
    const result = await this.httpService.get(uri, ShowUserInfo, handleError);
    return result;
  }

  public async deleteUser(userId: number): Promise<Request> {
    const uri = `users/${userId}`;
    return await this.httpService.delete(uri, Request);
  }

  public async getScoreboard(): Promise<UserWithScore[]> {
    const uri = 'users/scoreboard';
    return await this.httpService.getArray(uri, UserWithScore, false);
  }
}
