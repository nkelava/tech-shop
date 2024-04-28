<script setup>
import { computed, onMounted, ref } from "vue";
import { axiosPublic } from "@/api/axios";
import ReviewDialog from "@/components/review/ReviewDialog.vue";
import { formatDate } from "@/helpers/formatDate.js";

const props = defineProps(["product", "update"]);
const reviews = ref([]);
const pageState = ref({
  currentPage: 1,
  itemsPerPage: 5,
});

const totalPageCount = computed(() =>
  Math.ceil(reviews.value.length / pageState.value.itemsPerPage)
);

const getReviews = async () => {
  await axiosPublic
    .get(`/reviews/${props.product.id}`)
    .then((response) => {
      reviews.value = response.data;
    })
    .catch((error) => console.log(error));
};

onMounted(async () => {
  await getReviews();
});

const currentPageItems = computed(() => {
  return reviews.value.slice(
    (pageState.value.currentPage - 1) * pageState.value.itemsPerPage,
    pageState.value.currentPage * pageState.value.itemsPerPage
  );
});

const reviewDialogActive = ref(false);

async function toggleDialog() {
  reviewDialogActive.value = !reviewDialogActive.value;
  props.update();
  await getReviews();
}
</script>

<template>
  <v-card>
    <v-list v-if="currentPageItems.length > 0" lines="10">
      <v-list-item v-for="(review, i) in currentPageItems" :key="i">
        <div class="review">
          <span class="review__name">{{ review.email }}</span>
          <v-rating v-model="review.rate" size="small" density="compact" readonly />
          <span class="review__text">{{ review.comment }}</span>
          <div class="review__date">{{ formatDate(review.createdAt) }}</div>
        </div>
        <v-divider></v-divider>
      </v-list-item>
      <v-container v-if="currentPageItems.length > 0">
        <v-row justify="center">
          <v-col cols="10">
            <v-container class="max-width">
              <v-pagination v-model="pageState.currentPage" class="my-4" :length="totalPageCount" />
            </v-container>
          </v-col>
        </v-row>
      </v-container>
      <review-dialog
        v-model="reviewDialogActive"
        @toggleDialog="toggleDialog"
        :product="props.product"
      />
    </v-list>
    <v-card-text v-else> Be the first to leave a review. </v-card-text>
    <div class="d-flex justify-end">
      <v-btn class="review__btn" @click="reviewDialogActive = !reviewDialogActive">
        Add Review
      </v-btn>
    </div>
  </v-card>
</template>

<style scoped>
.v-card * {
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
  gap: 5px;
}

.review__name {
  font-weight: bold;
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

.review__date {
  display: flex;
  justify-content: flex-end;
  align-items: center;
}
</style>
