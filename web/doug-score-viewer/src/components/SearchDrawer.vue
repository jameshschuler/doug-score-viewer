<template>
  <div>
    <div class="slideout">
      <div class="slideout-header">
        <h1 class="is-size-4 has-text-weight-semibold">Search DougScores</h1>
        <button href="#" class="button is-ghost slideout-close" title="Close" @click="store.toggleSearchDrawer()">
          <i class="fa fa-times fa-lg"></i>
        </button>
      </div>
      <div class="columns">
        <div class="column is-10 is-offset-1">
          <div class="mb-3" v-if="!searching && appError">
            <Notification :dismissible="false" :appError="appError" />
          </div>
          <Message
            message="<p><b>Tip!</b> Press <kbd class='kbc-button kbc-button-xxs'>Ctrl</kbd> + <kbd class='kbc-button kbc-button-xxs'>q</kbd> to toggle this search drawer.</p>"
          />
          <form class="p-3" @submit.prevent="handleSearch">
            <div class="is-flex is-justify-content-space-between">
              <div class="field is-fullwidth">
                <label class="label">Min. Year</label>
                <div class="control">
                  <div class="select is-fullwidth">
                    <select v-model="formData.minYear">
                      <option v-for="option in yearOptions" v-bind:value="option.value">
                        {{ option.text }}
                      </option>
                    </select>
                  </div>
                </div>
              </div>
              <div class="field is-fullwidth ml-3">
                <label class="label">Max. Year</label>
                <div class="control">
                  <div class="select is-fullwidth">
                    <select v-model="formData.maxYear">
                      <option v-for="option in yearOptions" v-bind:value="option.value">
                        {{ option.text }}
                      </option>
                    </select>
                  </div>
                </div>
              </div>
            </div>

            <DynamicDropdown label="Make" :data-endpoint="getMakeOptions" v-model:selectedValue="formData.make" />
            <DynamicDropdown
              label="Model"
              :data-endpoint="getModelOptions"
              :disabled="isNullEmptyOrWhitespace(formData.make)"
              :params="formData.make"
              v-model:selectedValue="formData.model"
            />

            <CountryTags v-model:originCountries="formData.originCountries" />

            <div class="field is-fullwidth">
              <label class="label">Sort by</label>
              <div class="control">
                <div class="select is-fullwidth">
                  <select v-model="formData.sortByOption">
                    <option v-for="option in sortByOptions" :value="option.value">
                      {{ option.text }}
                    </option>
                  </select>
                </div>
              </div>
            </div>

            <div class="field is-grouped">
              <div class="control mt-5">
                <button :class="{ 'is-loading': searching }" type="submit" class="button is-success is-outlined">Search</button>
              </div>
              <div class="control mt-5">
                <button class="button is-outlined is-info" @click="resetForm" type="button">Reset</button>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
    <div class="slideout-mask"></div>
  </div>
</template>
<script setup lang="ts">
import { computed, ref } from "vue";
import { useRouter } from "vue-router";
import { Countries, initialSearchQuery } from "../constants";
import { SortBy, sortByOptions } from "../constants/sortOptions";
import { AppError } from "../models/common";
import { Country, SelectableCountry } from "../models/country";
import { AppErrorType } from "../models/enums/error";
import { SearchQuery } from "../models/searchQuery";
import { getMakeOptions, getModelOptions } from "../services/dataService";
import { searchDougScores } from "../services/dougScoreService";
import { store } from "../store";
import { getUrlSearchParams } from "../store/actions";
import { getYearOptions } from "../utils/options";
import { isNullEmptyOrWhitespace } from "../utils/strings";
import CountryTags from "./CountryTags.vue";
import DynamicDropdown from "./DynamicDropdown.vue";
import Message from "./Message.vue";
import Notification from "./Notification.vue";

const router = useRouter();
const yearOptions = computed(() => getYearOptions());

const formData = ref<SearchQuery>(initialSearchQuery);

if (store.currentSearchQuery !== null) {
  formData.value = store.currentSearchQuery;
}

const appError = ref<AppError>();
const searching = ref<boolean>(false);

async function handleSearch() {
  searching.value = true;
  const response = await searchDougScores({ ...formData.value });
  if (response.error && response.error.errorType === AppErrorType.BadRequest) {
    appError.value = response.error;
  } else {
    store.toggleSearchDrawer();
    store.setSearchResults(response.data);
    store.setCurrentSearchQuery({ ...formData.value });

    const params = getUrlSearchParams(store.currentSearchQuery);
    await router.push(`/search/results${params}`);
  }

  searching.value = false;
}

function resetForm() {
  formData.value = {
    make: "",
    model: "",
    minYear: "1960",
    maxYear: new Date().getUTCFullYear().toString(),
    originCountries: [
      ...(Countries.map((c: Country) => {
        return {
          ...c,
          selected: true,
        };
      }) as SelectableCountry[]),
    ],
    sortByOption: SortBy.TotalDougScoreDesc,
  };
  appError.value = undefined;
}
</script>
<style lang="scss" scoped>
$gray: #868e96;
$gray-light: lighten($gray, 10%);
$gray-lighter: lighten($gray-light, 10%);

@mixin drop-shadow($x: 0, $y: 1px, $blur: 2px, $spread: 0, $alpha: 0.25) {
  box-shadow: $x $y $blur $spread rgba(0, 0, 0, $alpha);
}

@mixin opacity($opacity: 0.5) {
  opacity: $opacity;
}

.is-fullwidth {
  width: 100%;
}

.slideout {
  position: fixed;
  top: 0;
  left: 0;
  bottom: 0;
  width: 35em;
  background-color: #ffffff;
  border-right: 0.063em solid lighten($gray-light, 30%);
  @include drop-shadow(1px, 0, 8px, 0, 0.5);
  z-index: 1000;
  transform: translateX(-100%);
  transition: transform 0.3s ease, width 0.3s ease;
  overflow-y: scroll;
  overflow-x: hidden;

  .slideout-header {
    padding: 1.2em 1em 1.2em 1.7em;
    display: flex;
    justify-content: space-between;

    .slideout-close {
      display: inline-block;
    }
  }
}

.slideout-mask {
  position: fixed;
  overflow: hidden;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #000000;
  z-index: 999;
  @include opacity(0);
  visibility: hidden;
  transition: opacity 0.3s ease, visibility 0.3s ease;
}

.slideout-open {
  .slideout-mask {
    @include opacity(0.7);
    visibility: visible;
  }

  .slideout {
    transform: translateX(0);
  }
}

@media (max-width: 768px) {
  .slideout-open {
    .slideout {
      width: 100vw;
    }
  }
}
</style>
