import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { TestingErrorComponent } from './testing-error/testing-error.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    NavBarComponent,
    TestingErrorComponent,
    ServerErrorComponent,
    NotFoundComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }),
  ],
  exports: [
    NavBarComponent,
    TestingErrorComponent,
    ServerErrorComponent,
    NotFoundComponent,
  ],
})
export class CoreModule {}
