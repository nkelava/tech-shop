<script setup>
import { computed, ref } from "vue";
import { reviews } from "@/database/seed/reviews";

const pageState = ref({
  currentPage: 1,
  itemsPerPage: 5,
});
const totalPageCount = computed(() => Math.ceil(reviews.length / pageState.value.itemsPerPage));

const currentPageItems = computed(() => {
  return reviews.slice(
    (pageState.value.currentPage - 1) * pageState.value.itemsPerPage,
    pageState.value.currentPage * pageState.value.itemsPerPage
  );
});

const rating = ref(1);
const message = ref("");

function handleReview() {
  if (message.value.length > 0) {
    console.log(`User: user; Rating: ${rating.value}; Message: ${message.value}`);
  }
}
</script>

<template>
  <v-card>
    <v-list lines="10">
      <v-list-item v-for="(review, i) in currentPageItems" :key="i">
        <div class="review">
          <span class="review__name">{{ review.name }}</span>
          <div class="review__info">
            <v-rating v-model="review.rating" size="small" density="compact" readonly />
            <p>{{ new Date().toJSON().slice(0, 10).replace(/-/g, "/") }}</p>
          </div>
          <span class="review__text">{{ review.review }}</span>
        </div>
        <v-divider inset></v-divider>
      </v-list-item>
      <v-container>
        <v-row justify="center">
          <v-col cols="10">
            <v-container class="max-width">
              <v-pagination v-model="pageState.currentPage" class="my-4" :length="totalPageCount" />
            </v-container>
          </v-col>
        </v-row>
      </v-container>
      <div class="review-input ts-container">
        <h2>Leave your review</h2>
        <span class="review__rating">
          Rating: <v-rating v-model="rating" density="compact" />
        </span>
        <v-textarea
          v-model="message"
          class="review__textarea"
          label="Your review"
          variant="outlined"
          prepend-inner-icon="mdi-comment"
          hide-details="true"
          no-resize
          clearable
          @keydown.enter.prevent
        ></v-textarea>
        <v-btn class="review__btn" @click="handleReview">Submit</v-btn>
      </div>
    </v-list>
  </v-card>
</template>

<style scoped>
.v-list {
  background-color: var(--ts-c-bg-light);
  color: var(--ts-c-text-dark);
}
.v-list-item {
  margin: auto;
}

.v-list-item hr {
  margin: 20px auto;
}

.review {
  display: flex;
  flex-direction: column;
}

.review__name {
  font-weight: bold;
}

.review__info {
  display: flex;
  align-items: center;
  gap: 5px;
}

.review__text {
  margin-top: 10px;
}

.review-input {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  gap: 10px;
}

.review__rating {
  display: flex;
  align-items: center;
  gap: 10px;
}

.review__textarea {
  width: 100%;
  max-width: 400px;
  font-weight: 14px;
}

::v-deep .v-label {
  font-size: 12px;
}

.review__btn {
  text-transform: capitalize;
  background-color: var(--ts-c-bg-dark);
  color: var(--ts-c-text-light);
}
</style>
