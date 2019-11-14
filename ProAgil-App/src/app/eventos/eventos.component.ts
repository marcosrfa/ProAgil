import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {
  eventos: any = [{
    EventoId: 1,
    Tema: 'Angular',
    Local: 'BH'
  }, {
    EventoId: 2,
    Tema: '.NET Core',
    Local: 'SP'
  }, {
    EventoId: 1,
    Tema: 'Angular e .NET Core',
    Local: 'RJ'
  }];
  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
    this.getEventos();
  }

  getEventos() {
    this.eventos = this.httpClient.get('http://localhost:5000/api/values')
      .subscribe(response => {
        console.log(response);
        this.eventos = response;
      }, error => console.log(error)
    );
  }

}
