/*
 * Copyright (C) 2023 The Android Open Source Project
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package com.example.android.trackr.ui.expense

import android.os.Bundle
import android.view.View
import androidx.fragment.app.Fragment
import androidx.fragment.app.viewModels
import com.example.android.trackr.R
import com.example.android.trackr.databinding.ExpenseFragmentBinding
import com.example.android.trackr.ui.dataBindings
import com.example.android.trackr.ui.utils.repeatWithViewLifecycle
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.coroutines.flow.collect
import kotlinx.coroutines.launch

@AndroidEntryPoint
class ExpenseFragment : Fragment(R.layout.expense_fragment) {

    private val viewModel: ExpenseViewModel by viewModels()
    private val binding by dataBindings(ExpenseFragmentBinding::bind)

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        val adapter = ExpenseAdapter(
            onUploadClick = { expense -> viewModel.uploadExpense(expense) }
        )
        binding.expenseList.adapter = adapter

        binding.fabAddExpense.setOnClickListener {
            viewModel.addExpense(
                title = "Client Dinner",
                amount = 85.00,
                category = "Meals",
                receiptUri = "receipt_captured_${System.currentTimeMillis()}.jpg",
                locationInfo = "Conference Center"
            )
        }

        repeatWithViewLifecycle {
            launch {
                viewModel.expenses.collect {
                    adapter.submitList(it)
                }
            }
        }
    }
}
