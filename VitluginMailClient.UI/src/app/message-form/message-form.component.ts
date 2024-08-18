import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { MessageService } from '../services/message.service';
import { HttpClient } from '@angular/common/http';
import { RecaptchaModule, RECAPTCHA_SETTINGS, RecaptchaSettings } from 'ng-recaptcha';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

@Component({
  selector: 'app-message-form',
  standalone: true,
  imports: [CommonModule, FormsModule, RecaptchaModule],
  templateUrl: './message-form.component.html',
  styleUrls: ['./message-form.component.css'],
  providers: [
    { 
      provide: RECAPTCHA_SETTINGS, 
      useValue: { 
        siteKey: '6Ld7GSkqAAAAADXzDtTOJoxNd0hDMxZAnXQsa3uF'
      } as RecaptchaSettings 
    }
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA] ////
})
export class MessageFormComponent implements OnInit {
  name: string = '';
  email: string = '';
  phone: string = '';
  topicId: number = 0;
  newTopic: string = '';
  showNewTopicInput: boolean = false;
  text: string = '';
  topics: any[] = [];
  messageDetails: any = null;
  recaptchaToken: string | null = null;
  submitted: boolean = false; // штука чтобы валидашки работали при отправке формы

  constructor(
    private http: HttpClient,
    private messageService: MessageService,
  ) { }

  ngOnInit(): void {
    this.messageService.getTopics().subscribe(
      data => this.topics = data,
      error => console.error('Error: ', error)
    );
  }

  onTopicChange(event: any): void {
    this.topicId = +event.target.value;
    this.showNewTopicInput = this.topicId === -1;
  }

  submitForm(form: NgForm): void {
    // проверка токена капчи перед отправкой
    this.submitted = true;
    if (form.invalid) {
      return;
    }

    if (!this.recaptchaToken) {
      console.error('reCAPTCHA token is missing');
      return;
    }

    if (this.showNewTopicInput && this.newTopic) {
      this.messageService.createTopic({ Name: this.newTopic }).subscribe(
        response => {
          this.topicId = response.id;
          this.createMessage();
        },
        error => console.error('Error: ', error)
      );
    } else {
      this.createMessage();
    }
  }

  createMessage(): void {
    const input = {
      Name: this.name,
      Email: this.email,
      Phone: this.phone,
      TopicId: this.topicId,
      Text: this.text,
      RecaptchaToken: this.recaptchaToken, // передача токена капчи с сообщением
    };

    this.messageService.createMessage(input).subscribe(
      response => {
        this.getMessageDetails(response.id);
      },
      error => console.error('Error: ', error)
    );
  }

  getMessageDetails(messageId: number): void {
    this.messageService.getMessageDetails(messageId).subscribe(
      data => { 
      this.messageDetails = data;
      },
      error => console.error('Error: ', error)
    );
  }

  handleSuccess(token: string | null): void {  
    if (token) {
      this.recaptchaToken = token; // сохранение токена капчи если прошли вертификацию
    } else {
      console.error('reCAPTCHA token is null');
    }
  }

}
