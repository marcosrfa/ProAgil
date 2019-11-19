import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {
  // Propriedade
  _filtroLista: string;
  public get filtroLista(): string {
    return this._filtroLista;
  }
  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  eventosFiltrados: any = [];
  eventos: any = [ ];
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;

  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
    this.getEventos();
  }

  filtrarEventos(filtrarPor: string): any{
    filtrarPor = filtrarPor.toLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  alternarImagem(){
    this.mostrarImagem = !this.mostrarImagem;
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
