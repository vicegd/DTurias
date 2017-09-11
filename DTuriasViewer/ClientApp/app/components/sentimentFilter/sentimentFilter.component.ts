import { Component } from "@angular/core";
import { Repository } from "../app/models/repository";

@Component({
    selector: "sentiment-filter",
    templateUrl: "sentimentFilter.component.html"
})
export class SentimentFilterComponent {

    constructor(private repo: Repository) { }

    get currentSentiment(): number {
        return this.repo.filter.sentiment;
    }

    setCurrentSentiment(newSentiment: number) {
        this.repo.filter.sentiment = newSentiment;
        this.repo.getTweets();
    }
}
