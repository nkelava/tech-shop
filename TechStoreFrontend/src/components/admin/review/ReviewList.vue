<script setup>
import { computed, onMounted, ref, watch } from "vue";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import { ITEM_DELETE_FAIL, ITEM_DELETE_SUCCESS } from "@/constants/messages/delete";
import { ITEM_UPDATE_FAIL, ITEM_UPDATE_SUCCESS } from "@/constants/messages/update";
import ConfirmationDialog from "@/components/common/ConfirmationDialog.vue";

const reportedReviews = ref([]);
const toast = useToast();
const selectedItemId = ref(null);
const showDialog = ref(false);
const searchQuery = ref("");
const pageState = ref({
  currentPage: 1,
  itemsPerPage: 10,
});

onMounted(() => reloadReportedReviews());

async function reloadReportedReviews() {
  const resp = await axiosPrivate.get("/reviews/reported").catch((error) => console.log(error));

  if (resp?.status !== 200) return;

  reportedReviews.value = resp.data;
}

const openConfirmationDialog = (id) => {
  selectedItemId.value = id;
  showDialog.value = true;
};

const deleteReview = async (reviewId) => {
  await axiosPrivate
    .delete(`/reviews/${reviewId}`)
    .then((resp) => {
      if (resp?.status === 200) {
        toast.success(ITEM_DELETE_SUCCESS);
      }
    })
    .catch(() => toast.error(ITEM_DELETE_FAIL));
  await reloadReportedReviews();
};

const removeFromReported = async (reviewId) => {
  await axiosPrivate
    .post(`/reviews/report/${reviewId}`, {
      isReported: false,
    })
    .then(async () => {
      toast.success(ITEM_UPDATE_SUCCESS);
      await reloadReportedReviews();
    })
    .catch((error) => {
      toast.error(ITEM_UPDATE_FAIL);
      console.log(error);
    });
};

const filteredReviews = computed(() => {
  return reportedReviews.value.filter((review) =>
    review?.email.toLowerCase().includes(searchQuery.value.trim().toLowerCase())
  );
});

// Watch for changes in the reported reviews array and adjust the current page if necessary
// This ensures that if items are deleted on the current page and it becomes empty,
// the user is redirected to the previous valid page.
watch(
  () => reportedReviews.value,
  () => {
    const totalItems = filteredReviews?.value.length;
    const maxPage = Math.ceil(totalItems / pageState.value.itemsPerPage);

    if (pageState.value.currentPage > maxPage) {
      pageState.value.currentPage = maxPage || 1;
    }
  },
  { immediate: true, deep: true }
);

const totalPageCount = computed(() =>
  Math.ceil(filteredReviews.value.length / pageState.value.itemsPerPage)
);

const currentPageItems = computed(() => {
  return filteredReviews.value.slice(
    (pageState.value.currentPage - 1) * pageState.value.itemsPerPage,
    pageState.value.currentPage * pageState.value.itemsPerPage
  );
});
</script>

<template>
  <v-container class="dashboard__container">
    <v-row class="header-search">
      <h2>Reported Review List</h2>
      <v-text-field
        v-model="searchQuery"
        class="header-search__input"
        label="Search by email..."
        hide-details="true"
        density="compact"
        variant="outlined"
      />
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
          <tr v-for="(review, id) in currentPageItems" :key="id">
            <td :title="review?.email">{{ review?.email }}</td>
            <td :title="review?.comment">{{ review?.comment }}</td>
            <td class="d-flex align-center">
              <v-btn
                color="green"
                icon="mdi-check"
                size="32"
                title="Remove from reported"
                alt="Remove from reported"
                @click="removeFromReported(review?.id)"
              />
              <v-btn
                class="ml-2"
                color="red"
                icon="mdi-delete"
                size="32"
                title="Delete"
                alt="Delete"
                @click="openConfirmationDialog(review?.id)"
              />
            </td>
          </tr>
          <tr v-if="reportedReviews.length < 1">
            <td>Nothing to see here yet.</td>
          </tr>
        </tbody>
      </v-table>
      <v-container v-if="reportedReviews.length > pageState.itemsPerPage">
        <v-row justify="center">
          <v-col cols="10">
            <v-container class="max-width">
              <v-pagination v-model="pageState.currentPage" class="my-4" :length="totalPageCount" />
            </v-container>
          </v-col>
        </v-row>
      </v-container>
    </v-row>
    <confirmation-dialog
      title="Confirm Deletion"
      content="Are you sure you want to delete this item?"
      confirmText="Delete"
      :showDialog="showDialog"
      :itemId="selectedItemId"
      @update:showDialog="showDialog = $event"
      @confirm="deleteReview"
    />
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
