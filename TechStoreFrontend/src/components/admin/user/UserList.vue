<script setup>
import { computed, onMounted, ref, watch } from "vue";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import { ITEM_DELETE_FAIL, ITEM_DELETE_SUCCESS } from "@/constants/messages/delete";
import { ITEM_UPDATE_FAIL, ITEM_UPDATE_SUCCESS } from "@/constants/messages/update";
import ConfirmationDialog from "@/components/common/ConfirmationDialog.vue";

const users = ref([]);
const toast = useToast();
const selectedItemEmail = ref(null);
const showDeleteDialog = ref(false);
const showPromoteDialog = ref(false);
const showDemoteDialog = ref(false);
const searchQuery = ref("");
const pageState = ref({
  currentPage: 1,
  itemsPerPage: 10,
});

onMounted(() => reloadUsers());

async function reloadUsers() {
  const resp = await axiosPrivate.get("/users/all").catch((error) => console.log(error));

  if (resp.status !== 200) return;
  users.value = resp.data;
}

const openConfirmationDialog = (email, dialogType) => {
  selectedItemEmail.value = email;

  switch (dialogType) {
    case "delete":
      showDeleteDialog.value = true;
      break;
    case "promote":
      showPromoteDialog.value = true;
      break;
    case "demote":
      showDemoteDialog.value = true;
      break;
  }
};

const deleteUser = async (email) => {
  await axiosPrivate
    .delete(`/users/${email}`)
    .then((resp) => {
      if (resp.status === 200) {
        toast.success(ITEM_DELETE_SUCCESS);
      }
    })
    .catch(() => toast.error(ITEM_DELETE_FAIL));
  await reloadUsers();
};

const promoteToAdmin = async (email) => {
  await axiosPrivate
    .post(`/users/promote/${email}`)
    .then((resp) => {
      if (resp.status === 200) {
        toast.success(ITEM_UPDATE_SUCCESS);
      }
    })
    .catch(() => toast.error(ITEM_UPDATE_FAIL));
  await reloadUsers();
};

const demoteToUser = async (email) => {
  await axiosPrivate
    .post(`/users/demote/${email}`)
    .then((resp) => {
      if (resp.status === 200) {
        toast.success(ITEM_UPDATE_SUCCESS);
      }
    })
    .catch(() => toast.error(ITEM_UPDATE_FAIL));
  await reloadUsers();
};

const filteredUsers = computed(() => {
  return users.value.filter((user) =>
    user?.info?.firstName
      .concat(" ", user?.info?.lastName)
      .toLowerCase()
      .includes(searchQuery.value.trim().toLowerCase())
  );
});

// Watch for changes in the users array and adjust the current page if necessary
// This ensures that if items are deleted on the current page and it becomes empty,
// the user is redirected to the previous valid page.
watch(
  () => users.value,
  () => {
    const totalItems = filteredUsers?.value.length;
    const maxPage = Math.ceil(totalItems / pageState.value.itemsPerPage);

    if (pageState.value.currentPage > maxPage) {
      pageState.value.currentPage = maxPage || 1;
    }
  },
  { immediate: true, deep: true }
);

const totalPageCount = computed(() =>
  Math.ceil(filteredUsers?.value.length / pageState.value.itemsPerPage)
);

const currentPageItems = computed(() => {
  return filteredUsers?.value.slice(
    (pageState.value.currentPage - 1) * pageState.value.itemsPerPage,
    pageState.value.currentPage * pageState.value.itemsPerPage
  );
});
</script>

<template>
  <v-container class="dashboard__container">
    <v-row class="header-search">
      <h2>User List</h2>
      <v-text-field
        v-model="searchQuery"
        class="header-search__input"
        label="Search..."
        hide-details="true"
        density="compact"
        variant="outlined"
      />
    </v-row>
    <v-row>
      <v-table class="dashboard__table">
        <thead>
          <tr>
            <th class="text-left">First Name</th>
            <th class="text-left">Last Name</th>
            <th class="text-left">Email</th>
            <th class="text-left">Confirmed</th>
            <th class="text-left">Admin</th>
            <th class="text-left">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(user, id) in currentPageItems" :key="id">
            <td>{{ user?.info?.firstName }}</td>
            <td>{{ user?.info?.lastName }}</td>
            <td :title="user?.info?.email">{{ user?.info?.email }}</td>
            <td class="text-capitalize">{{ user?.info?.emailConfirmed }}</td>
            <td class="text-capitalize">{{ user?.isAdmin }}</td>
            <td class="d-flex align-center">
              <v-btn
                color="orange"
                icon="mdi-account-star"
                size="32"
                title="Promote to admin"
                alt="Promote to admin"
                @click="openConfirmationDialog(user?.info?.email, 'promote')"
              />
              <v-btn
                class="ml-2"
                color="yellow"
                icon="mdi-account-minus"
                size="32"
                title="Demote to user"
                alt="Demote to user"
                @click="openConfirmationDialog(user?.info?.email, 'demote')"
              />
              <v-btn
                class="ml-2"
                color="red"
                icon="mdi-delete"
                size="32"
                title="Delete"
                alt="Delete"
                @click="openConfirmationDialog(user?.info?.email, 'delete')"
              />
            </td>
          </tr>
          <tr v-if="users.length < 1">
            <td>Nothing to see here yet.</td>
          </tr>
        </tbody>
      </v-table>
      <v-container v-if="users.length > pageState.itemsPerPage">
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
      title="Promote account"
      content="Are you sure you want to promote this account?"
      confirmText="Promote"
      :showDialog="showPromoteDialog"
      :itemId="selectedItemEmail"
      @update:showDialog="showPromoteDialog = $event"
      @confirm="promoteToAdmin"
    />
    <confirmation-dialog
      title="Demote Account"
      content="Are you sure you want to demote this account?"
      confirmText="Demote"
      :showDialog="showDemoteDialog"
      :itemId="selectedItemEmail"
      @update:showDialog="showDemoteDialog = $event"
      @confirm="demoteToUser"
    />
    <confirmation-dialog
      title="Confirm Deletion"
      content="Are you sure you want to delete this item?"
      confirmText="Delete"
      :showDialog="showDeleteDialog"
      :itemId="selectedItemEmail"
      @update:showDialog="showDeleteDialog = $event"
      @confirm="deleteUser"
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
