import { AuthService } from './services';

export class AppConfig {
  public baseUrl: string | undefined = undefined;
  public clientId: string | undefined = undefined;
}

export class ConfigService {
  private settings: AppConfig | undefined = undefined;

  public get config(): AppConfig {
    return this.settings;
  }

  public loadConfig(authService: AuthService): Promise<any> {
    return new Promise(async (resolve, _) => {
      const response = await fetch('./assets/configs/.config.json');
      const remoteConfig: AppConfig = await response.json();
      this.settings = remoteConfig;
      gapi.load('auth2', () => {
        const auth2 = gapi.auth2.init({
          client_id: this.settings.clientId,
          scope: 'email',
          ux_mode: 'redirect'
        });
        authService.setAuth2(auth2);
        resolve(remoteConfig);
      });
    });
  }
}
