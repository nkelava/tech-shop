<script setup>
import { onMounted, ref } from "vue";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import { ITEM_DELETE_FAIL, ITEM_DELETE_SUCCESS } from "../../../constants/messages/delete";

const reportedReviews = ref([]);
const toast = useToast();

onMounted(() => reloadReportedReviews());

async function reloadReportedReviews() {
  const resp = await axiosPrivate.get("/reviews/reported").catch((error) => console.log(error));

  if (resp.status !== 200) return;

  reportedReviews.value = resp.data;
}

async function deleteReview(reviewId) {
  await axiosPrivate
    .delete(`/reviews/${reviewId}`)
    .then((resp) => {
      if (resp.status === 200) {
        toast.success(ITEM_DELETE_SUCCESS);
      }
    })
    .catch(() => toast.error(ITEM_DELETE_FAIL));
  await reloadReportedReviews();
}

// TODO: Add pagination (10 items per page)
</script>
<template>
  <v-container class="dashboard__container">
    <v-row>
      <h2>Reported Review List</h2>
    </v-row>
    <v-row>
      <v-table class="dashboard__table">
        <thead>
          <tr>
            <th class="text-left">Email</th>
            <th class="text-left">Comment</th>
            <th class="text-left">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(review, id) in reportedReviews" :key="id">
            <td :title="review?.email">{{ review?.email }}</td>
            <td :title="review?.comment">{{ review?.comment }}</td>
            <td class="d-flex align-center">
              <v-btn
                color="red"
                icon="mdi-delete"
                size="32"
                title="Delete"
                alt="Delete"
                @click="deleteReview(review?.id)"
              />
            </td>
          </tr>
          <tr v-if="reportedReviews.length < 1">
            <td>There are no records.</td>
          </tr>
        </tbody>
      </v-table>
    </v-row>
  </v-container>
</template>

<style scoped>
.dashboard__table {
  width: 100%;
  margin-top: 1rem;
  color: var(--ts-c-text-light);
  background: transparent;
  border: 1px solid var(--ts-c-primary-dark);
  border-radius: 10px;
}

thead {
  background: var(--ts-c-primary-dark);
}

tr {
  vertical-align: center;
}

th {
  color: var(--ts-c-text-light) !important;
}

td {
  max-width: 500px !important;
  overflow: hidden;
  text-overflow: ellipsis;
}
</style>
