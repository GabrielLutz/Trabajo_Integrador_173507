import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ErrorMessageComponent } from './components/error-message/error-message.component';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';
import { LoadingComponent } from './components/loading/loading.component';
import { FormatCedulaPipe } from './pipes/format-cedula.pipe';
import { FormatDatePipe } from './pipes/format-date.pipe';

@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    LoadingComponent,
    ErrorMessageComponent,
    FormatCedulaPipe,
    FormatDatePipe
  ],
  imports: [CommonModule, RouterModule],
  exports: [
    HeaderComponent,
    FooterComponent,
    LoadingComponent,
    ErrorMessageComponent,
    FormatCedulaPipe,
    FormatDatePipe
  ]
})
export class SharedModule {}
