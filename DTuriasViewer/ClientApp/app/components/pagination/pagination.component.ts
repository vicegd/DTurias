import { Component } from "@angular/core";
import { Repository } from "../app/models/repository";

@Component({
    selector: "pagination",
    templateUrl: "pagination.component.html"
})
export class PaginationComponent {

    constructor(private repo: Repository) { }

    get current(): number {
        return this.repo.pagination.currentPage;
    }

    get pages(): number[] {
        if (this.repo.tweets != null) {
            return Array(Math.ceil(this.repo.count / this.repo.pagination.perPage))
                .fill(0).map((x, i) => i + 1);
        } else {
            return [];
        }
    }

    changePage(newPage: number) {
        this.repo.pagination.currentPage = newPage;
        this.repo.getTweets();
    }
}